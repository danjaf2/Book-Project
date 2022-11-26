const express = require('express')
const router = express.Router()

router.get('/', (req, res) => {
    res.send('Placeholder')
})

router.post('/', (req, res) => {
    res.send('Placeholder')
})

router.put('/', (req, res) => {
    res.send('Placeholder')
})

router.delete('/', (req, res) => {
    res.send('Placeholder')
})

module.exports = router