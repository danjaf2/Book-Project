const express = require("express");
const app = express();
const db = require("../models");
const controller = require("../controllers/book.controller");
const UserBookShelfdb = db.userBookShelf;
const UserGenres = db.userGenres;
const Booksdb = db.book;
const router = express.Router();

const maxNumberOfRecommendedBooks = 20;

const print = (i) => {
  console.log(i);
};

// Get book recommendations based on the user's favorite genres.
// The recommendation implementation is to select a certain number of books
// of the selected genres, and limit the total amount of books to around 20.
router.post("/books/recommendations", async (req, res) => {
  // req: {"genres": [array of genres], "userId": <id>}
  const userId = req.body.userId;

  try {
    const userGenres = await UserGenres.findAll({
      where: {
        userId: userId,
      },
      attribute: ["genre"],
    });
    const genres = userGenres.map((e) => e.genre);
    const allBooksGenres = [];
    const allBooks = await Booksdb.findAll({
      attributes: ["id", "title", "author", "genre", "cover"],
    });

    print(genres);
    for (let i = 0; i < genres.length; i++) {
      const allBooksOfGenre = allBooks.filter(
        (book) => book.genre === genres[i]
      );
      allBooksGenres.push(allBooksOfGenre);
    }

    const shuffle = (array) => {
      return array.sort(() => 0.5 - Math.random());
    };

    // Based on the number of books per genre
    const numberOfBooksPerGenre = Math.floor(
      maxNumberOfRecommendedBooks / genres.length
    );

    const recommendedBooks = [];
    for (let i = 0; i < allBooksGenres.length; i++) {
      const books = allBooksGenres[i];

      // Shuffle the books array of the current genre
      // and take some numberOfBooksPerGenre number of books.
      // If the user already has a book that is in one of their shelves,
      // restart the process until all books are not in neither of their shelves.
      shuffle(books);
      const booksToGiveForGenre = [];
      for (let i = 0; i < books.length; i++) {
        const userBookShelf = await UserBookShelfdb.findOne({
          where: { userId: userId, bookId: books[i].id },
        });
        if (!userBookShelf) {
          booksToGiveForGenre.push(books[i]);
        }

        if (booksToGiveForGenre.length == numberOfBooksPerGenre)
            break;
      }

      recommendedBooks.push(...booksToGiveForGenre);
    }

    // If there are not enough books for the recommendation shelf
    // based on the selected genres add random books.
    if (recommendedBooks.length < numberOfBooksPerGenre) {
      const remainingBooks = allBooks.filter(
        (book) => !recommendedBooks.find((b) => book === b)
      );

      shuffle(remainingBooks);
      while (maxNumberOfRecommendedBooks - recommendedBooks.length != 0) {
        const book = remainingBooks[i];
        i++;
        if (
          await UserBookShelfdb.findOne({
            where: { bookId: book.id },
          })
        ) {
          continue;
        } else {
          recommendedBooks.push(book);
        }
      }
    }

    console.log(recommendedBooks.length);

    res.status(200).send(recommendedBooks);
  } catch (error) {
    console.log(error);
    res.status(400).send("Error in unlinking book.");
  }
});

router.get("/books", controller.getAllBooks);

module.exports = router;
