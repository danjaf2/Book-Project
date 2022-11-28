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

    const allBooksOfUser = await db.sequelize.query(
        'SELECT shelfId, bookId, title, author, genre, cover FROM userBookShelves ' + 
        'JOIN Books ON Books.id = userBookShelves.bookId ' +
        'WHERE userId = ' + userIdParam +  
        ' ORDER BY shelfId, genre');

    res.send(JSON.stringify(allBooksOfUser, null, 2));
})


// Get all books from a shelf for an user
router.get('/UserShelfBooks/:userId/:shelfId', async (req, res) => {
    const shelfIdParam = req.params.shelfId;
    const userIdParam = req.params.userId;

    const allBooksOfShelfOfUser = await db.sequelize.query(
        'SELECT bookId, title, author, genre, cover FROM userBookShelves ' + 
        'JOIN Books ON Books.id = userBookShelves.bookId ' +
        'WHERE userId = ' + userIdParam + ' AND shelfId = ' + shelfIdParam + 
        ' ORDER BY shelfId');

    res.send(JSON.stringify(allBooksOfShelfOfUser, null, 2));
})

router.post('/addBook/:shelfId/:userId', async (req, res) => {
    const shelfIdParam = req.params.shelfId;
    const userIdParam = req.params.userId;
    const { title, author, genre, cover } = req.query;

    // Find if book already exists, if not then create it 
    let bookRef = await Booksdb.findOne({ where: { title: title }});
    if (bookRef == null){
        bookRef = await Booksdb.create({ title, author, genre, cover });
    }
    
    // Add book into userBookShelf
    const addBookToShelf = await UserBookShelfdb.create({ 
        userId: userIdParam,
        shelfId: shelfIdParam,
        bookId: bookRef.id
    })

    if(addBookToShelf){
        res.status(201).send('Book created successfully.')
    }
    else {
        res.status(400).send('Error in creating book.')
    }
})

// router.put('/:shelfId/:userId', (req, res) => {
//     res.send('Placeholder')
// })

// router.delete('/:shelfId/:userId', (req, res) => {
//     res.send('Placeholder')
// })

module.exports = router
