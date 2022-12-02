const db = require("../models");
const config = require("../config/auth.config");
const Book = db.book;

const Op = db.Sequelize.Op;

exports.getAllBooks = async (req, res) => {
    try {
        const allBooks = await Book.findAll({
            attributes: ['id', 'title', 'author', 'genre', 'cover']
        });

        res.send(allBooks);
    } catch (error) {
        res.status(500).send({ message: error.message });
    }
}