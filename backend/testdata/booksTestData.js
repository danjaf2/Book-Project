const db = require("../src/models");
const Books = db.book;
const UserBookShelf = db.userBookShelf;

const jsonFile = require("./books.json");

module.exports = {
  seed: async function () {
    if (await Books.findOne()) {
      return;
    }
    
    Books.destroy({
        truncate: true
    });
    jsonFile.forEach(async (element, i) => {
      await Books.create({
        id: i,
        title: element["title"],
        author: element["author"],
        cover: `${element["id"]}.jpg`,
        genre: element["genre"],
      });
    });
  },
  seedUserBookShelves: async function () {
    const historyBooks = await Books.findAll({
      where: { genre: 'History' }
    });

    console.log(historyBooks.length);

    for (let i = 0; i < historyBooks.length - 1; i++) {
      await UserBookShelf.create({
        userId: 1,
        bookId: historyBooks[i].id,
        shelfId: 1
      })
    }
  },
};
