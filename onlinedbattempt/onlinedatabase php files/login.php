<?php

$servername = "localhost";
$username = "id9361653_dldvirtuallab";
$server_password = "IndecipherablyObfuscatedTExT";
$dbName = "id9361653_dldvirtuallab";


$email = $_POST["email"];
$password= $_POST["password"];

$conn = new mysqli($servername, $username, $server_password, $dbName);

if(!$conn){
	die("Connection Failed" . mysqli_connect_error());
}

$emailcheckquery = "SELECT email, salt, hash FROM student_account WHERE email = '" . $email . "';";

$emailcheck = mysqli_query($conn, $emailcheckquery) or die("2: Email Check Query Failed");

if(mysqli_num_rows($emailcheck) != 1){
	echo"Either no user with this email or more than 1";
	exit();
}
else{
	$info = mysqli_fetch_assoc($emailcheck);
	$salt = $info['salt'];
	$hash = $info['hash'];

	$loginhash = crypt($password, $salt);
	if($hash != $loginhash){
		echo"Incorrect password";
		exit();
	}
}

echo "0";


?>