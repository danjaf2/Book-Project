// From https://www.bezkoder.com/node-js-express-login-example/

module.exports = (sequelize, Sequelize) => {
  const Book = sequelize.define("books", {
    title: {
      type: Sequelize.STRING,
    },
    author: {
      type: Sequelize.STRING,
    },
    cover: {
      type: Sequelize.STRING,
    },
  });

  return Book;
};