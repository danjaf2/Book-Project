const express = require('express')
const router = express.Router()
const db = require("../models");
const UserBookShelfdb = db.userBookShelf;
const Booksdb = db.book;


/** 
 * Get all books and their shelves for an userId
 */
router.get('/UserAllBooks/:userId', async (req, res) => {
    const userIdParam = req.params.userId;
    try {
        const allBooksOfUser = await db.sequelize.query(
            'SELECT ubs.id, shelfId, bookId, title, author, genre, cover, favorited FROM userBookShelves as ubs ' +
            'JOIN Books ON Books.id = ubs.bookId ' +
            'WHERE userId = ' + userIdParam +
            ' ORDER BY shelfId, genre');

        res.status(200).send(JSON.stringify(allBooksOfUser, null, 2));
    } catch (error) {
        console.log(error)
        res.status(400).send("Error fetching books");
    }

})


// Get all books from a shelf for an user
router.get('/UserShelfBooks/:userId/:shelfId', async (req, res, next) => {
    const shelfIdParam = req.params.shelfId;
    const userIdParam = req.params.userId;
    try {
        const allBooksOfShelfOfUser = await db.sequelize.query(
            'SELECT ubs.id, bookId, title, author, genre, cover, favorited FROM userBookShelves as ubs ' +
            'JOIN Books ON Books.id = ubs.bookId ' +
            'WHERE userId = ' + userIdParam + ' AND shelfId = ' + shelfIdParam +
            ' ORDER BY shelfId');

        res.status(200).send(JSON.stringify(allBooksOfShelfOfUser, null, 2));
    } catch (e) {
        console.log(e)
        res.status(400).send("Error fetching books");
    }
})

/**
 * Add a new or preexisting book to this user's shelf
 */
router.post('/addBook/:shelfId/:userId', async (req, res) => {
    const shelfIdParam = req.params.shelfId;
    const userIdParam = req.params.userId;
    const { title, author, genre, cover, favorited } = req.query;

    try {
        // Find if book already exists in Books table, if not then create it 
        let bookRef = await Booksdb.findOne({ where: { title: title } });
        if (bookRef == null) {
            bookRef = await Booksdb.create({ title, author, genre, cover });
        }

        // Link book to user in userBookShelf table
        await UserBookShelfdb.create({
            userId: userIdParam,
            shelfId: shelfIdParam,
            bookId: bookRef.id,
            favorited
        })

        res.status(201).send('Book created successfully.')
    } catch (error) {
        console.log(error)
        res.status(400).send('Error in creating book.')
    }
})

// Move a book into a new shelf
router.put('/:userbookshelfId/:newShelfId', async (req, res) => {

    const userbookshelfId = req.params.userbookshelfId;
    const newShelfId = req.params.newShelfId;
    try {
        await UserBookShelfdb.update(
            { shelfId: newShelfId },
            { where: { id: userbookshelfId } }
        );

        res.status(200).send('Book changed successfully.')
    } catch (error) {
        console.log(error);
        res.status(400).send('Error in moving shelf of book.')
    }
})

// Toggle a book as favorite
router.put('/toggleFavorite/:userbookshelfId', async (req, res) => {
    const userbookshelfId = req.params.userbookshelfId;

    try {
        let entryRef = await UserBookShelfdb.findOne({ where: { id: userbookshelfId } });
        if (entryRef == null) { res.status(404).send('book shelf id not found!'); }

        const favoriteBook = await UserBookShelfdb.update(
            { favorited: !entryRef.favorited },
            { where: { id: userbookshelfId } }
        );

        res.status(200).send(JSON.stringify(favoriteBook, null, 2));
    } catch (error) {
        console.log(error);
        res.status(400).send('Error in favoriting book.')
    }
})

/**
 * Unlink a book from an user. (Does not delete from Books table)
 */
router.delete('/:userbookshelfId', async (req, res) => {

    const userbookshelfId = req.params.userbookshelfId;
    try {
        await UserBookShelfdb.destroy({
            where: { id: userbookshelfId }
        })

        res.status(200).send('Book unlinked successfully.')
    } catch (error) {
        console.log(error);
        res.status(400).send('Error in unlinking book.')
    }
})

module.exports = router
