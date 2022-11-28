const express = require("express");
const cors = require("cors");
const cookieSession = require("cookie-session");
const swaggerUi = require("swagger-ui-express");
const swaggerFile = require("./swagger-output.json");
const app = express();
const port = 3000;

const AllShelvesRoute = require('./src/routes/AllShelvesRoute');

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

app.use("/doc", swaggerUi.serve, swaggerUi.setup(swaggerFile));

const db = require("./src/models");
db.sequelize.sync().then(() => {
    initial();
});

const Shelf = db.shelf;

app.get("/", (req, res) => {
  res.send("Hello World!");
});

// debug only 
(async () => {
  const test1 = await db.sequelize.query('SELECT id, title, author, genre, cover FROM Books');
  const test2 = await db.sequelize.query('SELECT id,name FROM Shelves');
  const test3 = await db.sequelize.query('SELECT username FROM Users');
  const test4 = await db.sequelize.query('SELECT id, userId, bookId, shelfId FROM UserBookShelves');
  console.log(
      JSON.stringify(test1,null,2) +
      JSON.stringify(test2,null,2) + 
      JSON.stringify(test3,null,2) + 
      JSON.stringify(test4,null,2)
  )
})();



app.listen(port, () => {
  console.log(
    `Server is running!\nAPI documentation: http://localhost:${port}/doc`
  );
});

// Shelf routes
app.use('/api', AllShelvesRoute);

require("./src/routes/auth.routes")(app);
require("./src/routes/user.routes")(app);

function initial() {
    Shelf.create({
        id: 1,
        name: "Recommended"
    });

    Shelf.create({
        id: 2,
        name: "Favorites"
    });

    Shelf.create({
        id: 3,
        name: "To Read"
    });

    Shelf.create({
        id: 4,
        name: "Read"
    });

    Shelf.create({
        id: 5,
        name: "Reading"
    });
}