<?php

$servername = "localhost";
$username = "id9361653_dldvirtuallab";
$server_password = "IndecipherablyObfuscatedTExT";
$dbName = "id9361653_dldvirtuallab";

$Question = $_POST["Question"];
$Answer = $_POST["Answer"];


$conn = new mysqli($servername, $username, $server_password, $dbName);

if(!$conn){
	die("Connection Failed" . mysqli_connect_error());
}

$Questioncheckquery = "SELECT Question FROM FAQ WHERE Question = '" . $Question . "';";

$Questioncheck = mysqli_query($conn, $Questioncheckquery) or die("2: Name Check Query Failed");


if(mysqli_num_rows($Questioncheck) > 0){
	echo "3: This question already exists";
	exit();
}

$sqlquestion = "INSERT INTO FAQ(Question) VALUES('".$Question."')";

$resultquestion = mysqli_query($conn, $sqlquestion);

$Answercheckquery = "SELECT Answer FROM FAQ WHERE Answer = '" . $Answer . "';";

$Answercheck = mysqli_query($conn, $Answercheckquery) or die("2: Name Check Query Failed");


if(mysqli_num_rows($Answercheck) > 0){
	echo "3: This question already exists";
	exit();
}

$sqlanswer = "INSERT INTO FAQ(Answer) VALUES('".$Answer."')";

$resultanswer = mysqli_query($conn, $sqlanswer);

if(!$resultquestion || !$resultanswer){
	echo"There was an error";

}

else{
	echo"0";
}




?>