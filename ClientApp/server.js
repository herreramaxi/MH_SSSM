const express = require('express')
const app = express()
const path = require('path');

// Parse URL-encoded bodies (as sent by HTML forms)
app.use(express.urlencoded({ extended: true }))
// Parse JSON bodies (as sent by API clients)
app.use(express.json());

// if (process.env.NODE_ENV !== 'production') {
//     app.use(cors());
// }

// Serve static files
app.use(express.static(path.join(__dirname, 'build')));
app.use(express.static(path.join(__dirname, 'public')));
app.get('/', function (req, res) {
    res.sendFile(path.join(__dirname, 'public', 'index.html'));
});

app.get('/hello', (req, res) => {
    res.send("hello world!")
});

// Start the app by listening on the default Heroku port
app.listen(process.env.PORT || 3000);