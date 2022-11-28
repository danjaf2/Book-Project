const db = require("../src/models");
const Books = db.book;

const jsonFile = require("./books.json");

module.exports = {
  seed: function () {
    jsonFile.forEach((element, i) => {
      if (
        !Books.findOne({
          where: {
            title: element.title,
            author: element.author,
          },
        })
      ) {
        Books.create({
          id: i,
          title: element["title"],
          author: element["author"],
          cover: element["cover"],
        });
      }
    });
  },
};
