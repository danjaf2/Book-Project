const express = require("express");
const cors = require("cors");
const cookieSession = require("cookie-session");
const app = express();
const port = 3000;

const FavoriteShelfRoute = require('./src/routes/Shelves/FavoriteShelfRoute');
const ReadingShelfRoute = require('./src/routes/Shelves/ReadingShelfRoute');
const ReadShelfRoute = require('./src/routes/Shelves/ReadShelfRoute');
const RecommendationShelfRoute = require('./src/routes/Shelves/RecommendationShelfRoute');
const ToReadShelfRoute = require('./src/routes/Shelves/ToReadShelfRoute');

app.use(cors());

// parse requests of content-type - application/json
app.use(express.json());

// parse requests of content-type - application/x-www-form-urlencoded
app.use(express.urlencoded({ extended: true }));

app.use(
  cookieSession({
    name: "book-project-session",
    secret: "COMP354", // should use as secret environment variable
    httpOnly: true,
  })
);

const db = require("./src/models");
db.sequelize.sync();
const User = db.user;

app.get("/", (req, res) => {
  res.send("Hello World!");
});

// Shelf routes
app.use('/Favorite', FavoriteShelfRoute);
app.use('/Reading', ReadingShelfRoute);
app.use('/Read', ReadShelfRoute);
app.use('/Recommendation', RecommendationShelfRoute);
app.use('/ToRead', ToReadShelfRoute);


app.listen(port, () => {
  console.log(`Example app listening on port ${port}`);
});


require('./src/routes/auth.routes')(app);
require('./src/routes/user.routes')(app);

