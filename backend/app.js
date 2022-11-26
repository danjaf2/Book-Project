const express = require("express");
const cors = require("cors");
const cookieSession = require("cookie-session");
const swaggerUi = require("swagger-ui-express");
const swaggerFile = require("./swagger-output.json");
const app = express();
const port = 3000;

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

app.use("/", swaggerUi.serve, swaggerUi.setup(swaggerFile));

const db = require("./src/models");
db.sequelize.sync();
const User = db.user;

app.listen(port, () => {
  console.log(
    `Server is running!\nAPI documentation: http://localhost:${port}/doc`
  );
});

require("./src/routes/auth.routes")(app);
require("./src/routes/user.routes")(app);
