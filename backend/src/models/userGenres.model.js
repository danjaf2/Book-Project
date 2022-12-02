module.exports = (sequelize, Sequelize) => {
    const UserGenres = sequelize.define("userGenres", {
      userId: {
        type: Sequelize.INTEGER,
      },
      genre: {
        type: Sequelize.STRING,
      },
    });
  
    return UserGenres;
  };
  