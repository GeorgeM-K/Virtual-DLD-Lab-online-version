<?php

$servername = "localhost";
$username = "id9361653_dldvirtuallab";
$server_password = "IndecipherablyObfuscatedTExT";
$dbName = "id9361653_dldvirtuallab";

$email = $_POST["email"];
$grade = $_POST["grade"];
$lab = $_POST["Lab"];

$conn = new mysqli($servername, $username, $server_password, $dbName);

if(!$conn){
	die("Connection Failed" . mysqli_connect_error());
}

$emailcheckquery = "SELECT email FROM student_account WHERE email = '" . $email . "';";

$emailcheck = mysqli_query($conn, $emailcheckquery) or die("2: Email Check Query Failed");

if(mysqli_num_rows($emailcheck) == 0){
	echo "3: This email is not linked to an account";
	exit();
}

$Update = "UPDATE student_account SET $lab = $grade WHERE email = '".$email."';";
$Updatecheck = mysqli_query($conn, $Update) or die("3: Update check failed");


if(!$Updatecheck){
	echo"There was an error";

}

else{
	echo"0";
}




?>