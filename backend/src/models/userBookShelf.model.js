// From https://www.bezkoder.com/node-js-express-login-example/

module.exports = (sequelize, Sequelize) => {
  const UserBookShelf = sequelize.define("userBookShelf", {
    id: {
      type: Sequelize.INTEGER,
      primaryKey: true,
      autoIncrement: true
    },
    userId: {
      type: Sequelize.INTEGER,
      references: {
        model: 'users',
        key: 'id'
      }
    },
    bookId: {
      type: Sequelize.INTEGER,
      references: {
        model: 'books',
        key: 'id'
      }
    },
    shelfId: {
      type: Sequelize.INTEGER,
      references: {
        model: 'shelves',
        key: 'id'
      }
    },
    favorited: {
      type: Sequelize.BOOLEAN,
    },
  });

  return UserBookShelf;
};
