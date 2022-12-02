module.exports = {
  HOST: "localhost",
  USER: "user",
  PASSWORD: "password",
  DB: "testdb",
  dialect: "sqlite",
  storage: "./database3.sqlite3",
  pool: {
    max: 5,
    min: 0,
    acquire: 30000,
    idle: 10000,
  },
};
