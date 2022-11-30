const express = require('express')
const app = express()
const controller = require("../controllers/book.controller");

module.exports = function(app) {
    app.get('/api/books', controller.getAllBooks)
}