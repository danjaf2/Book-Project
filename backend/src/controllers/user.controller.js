const db = require("../models");
const config = require("../config/auth.config");
const UserGenres = db.userGenres;

exports.saveUserFavoriteGenres = async (req, res) => {
  try {
    const genres = req.body.genres;
    const userId = req.params.userId;

    await UserGenres.destroy({
      where: { userId: userId }
    });

    for (let i = 0; i < genres.length; i++) {
      const genre = genres[i];
      await UserGenres.create({
        userId: userId,
        genre: genre,
      });
    }
    res.status(200).send("Updated user's favorite genres");
  } catch (error) {
    console.log(error);
    res.status(400).send("Error in updating user's favorite genres.");
  }
};

exports.getUserFavoriteGenres = async (req, res) => {
  try {
    const userId = req.params.userId;

    const allUserGenresOfUser = await UserGenres.findAll({
      where: { userId: userId }
    });

    res.status(200).send({
      genres: allUserGenresOfUser.map(e => e.genre)
    });
  } catch (error) {
    console.log(error);
    res.status(400).send("Error in updating user's favorite genres.");
  }
};