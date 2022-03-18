<?php

$cors_whitelist = [
    'https://theindiedream.itch.io/transfer/',
    'https://theindiedream.itch.io/',
    'https://v6p9d9t4.ssl.hwcdn.net',
    'https://itch.io/',
    'https://itch.zone/',
];
  
if (in_array($_SERVER['HTTP_ORIGIN'], $cors_whitelist)) 
{
    header('Access-Control-Allow-Origin: ' . $_SERVER['HTTP_ORIGIN']);
}

define("DBHOST","localhost");
define("DBUSER","brandrz2_player");
define("DBPASS","4kd7Uy!]ePfU");
define("DBNAME","brandrz2_HighScoreDatabase");

$connection = new mysqli(DBHOST,DBUSER,DBPASS, DBNAME);

if (isset($_POST['retrieve_leaderboard']))
{
    $sql = "SELECT * FROM blaster_comets ORDER BY score DESC limit 10";

    $result = $connection->query($sql);
    $num_results = $result->num_rows;

    for ($i = 0; $i < $num_results; $i++) 
    {
        if (!($row = $result->fetch_assoc()))
            break;
        echo $row["name"];
        echo "\n";
        echo $row["score"];
        echo "\n";
    }

    $result->free_result();
}
elseif (isset($_POST['post_leaderboard']))
{

    $name = mysqli_escape_string($connection, $_POST['name']);
    $name = filter_var($name, FILTER_SANITIZE_STRING, FILTER_FLAG_STRIP_LOW | FILTER_FLAG_STRIP_HIGH);

    $score = $_POST['score'];

    $statement = $connection->prepare("INSERT INTO blaster_comets (name, score) VALUES (?, ?)");
    $statement->bind_param("si", $name, $score);

    $statement->execute();
    $statement->close();
}

$connection->close();

?>