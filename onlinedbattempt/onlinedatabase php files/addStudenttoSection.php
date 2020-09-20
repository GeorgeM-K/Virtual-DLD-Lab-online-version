<?php

$servername = "localhost";
$username = "id9361653_dldvirtuallab";
$server_password = "IndecipherablyObfuscatedTExT";
$dbName = "id9361653_dldvirtuallab";

$email = $_POST["email"];
$class = $_POST["class"];
$lab = $_POST["Lab"];


$conn = new mysqli($servername, $username, $server_password, $dbName);

if(!$conn){
	die("Connection Failed" . mysqli_connect_error());
}

$namecheckquery = "SELECT email FROM student_account WHERE email = '" . $email . "';";

$namecheck = mysqli_query($conn, $namecheckquery) or die("2: Name Check Query Failed");


if(mysqli_num_rows($namecheck) == 0){
	echo "3: This student does not exist";
	exit();
}

$Update = "UPDATE student_account SET $lab = '$class' WHERE email = '".$email."';";
$Updatecheck = mysqli_query($conn, $Update) or die("4: Update check failed");


if(!$Updatecheck){
	echo"There was an error";

}

else{
	echo"0";
}




?>