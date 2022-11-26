// From https://www.bezkoder.com/node-js-express-login-example/

module.exports = (sequelize, Sequelize) => {
  const Shelf = sequelize.define("shelves", {
    id: {
      type: Sequelize.INTEGER,
      primaryKey: true
    },
    name: {
      type: Sequelize.STRING,
    },
  });

  return Shelf;
};
