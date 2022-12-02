const db = require("../src/models");
const Books = db.book;

const jsonFile = require("./books.json");

module.exports = {
  seed: function () {
    if (Books.findOne()) {
      return;
    }
    
    Books.destroy({
        truncate: true
    });
    jsonFile.forEach((element, i) => {
      Books.create({
        id: i,
        title: element["title"],
        author: element["author"],
        cover: `${element["id"]}.jpg`,
        genre: element["genre"],
      });
    });
  },
};
