const express = require("express");
const app = express();
const db = require("../models");
const controller = require("../controllers/book.controller");
const UserBookShelfdb = db.userBookShelf;
const Booksdb = db.book;
const router = express.Router();

// Get book recommendations based on the selected genres
// The recommendation implementation is to select a certain number of books
// of the selected genres, and limit the total amount of books to around 20.
router.post("/books/recommendations", async (req, res) => {
  // req: {"genres": [array of genres], "userId": <id>}
  const genres = req.body.genres;
  const userId = req.body.userId;
  try {
    const allBooksGenres = [];
    const allBooks = await Booksdb.findAll({
      attributes: ["id", "title", "author", "genre", "cover"],
    });

    await genres.forEach(async (g) => {
      const allBooksOfGenre = await allBooks.filter(book => book.genre === g);
      allBooksGenres.push(allBooksOfGenre);
    });

    const shuffle = (array) => {
      return array.sort(() => 0.5 - Math.random());
    };

    // Based on the number of books per genre
    const numberOfBooksPerGenre = Math.floor(20 / genres.length);
    const recommendedBooks = [];
    allBooksGenres.forEach(async (books) => {
      let allGood = true;
      // Shuffle the books array of the current genre
      // and take some numberOfBooksPerGenre number of books.
      // If the user already has a book that is in one of their shelves,
      // restart the process until all books are not in neither of their shelves.
      while (allGood) {
        shuffle(books);
        const recommendedBooksForGenre = books.slice(0, numberOfBooksPerGenre);
        for (let i = 0; i < recommendedBooksForGenre.length; i++) {
          if (
            await UserBookShelfdb.findOne({
              where: { userId: userId, bookId: recommendedBooksForGenre[i].id },
            })
          ) {
            break;
          }
        }

        allGood = false;
        recommendedBooks.push(...recommendedBooksForGenre);
      }
    });

    // If there are not enough books for the recommendation shelf
    // based on the selected genres add random books.
    const remainingBooks = allBooks.filter(book => !recommendedBooks.find(b => book === b));
    shuffle(remainingBooks);
    recommendedBooks.push(...remainingBooks.slice(0, numberOfBooksPerGenre - recommendedBooks.length));

    res.status(200).send(recommendedBooks);
  } catch (error) {
    console.log(error);
    res.status(400).send("Error in unlinking book.");
  }
});

router.get("/books", controller.getAllBooks);

module.exports = router;