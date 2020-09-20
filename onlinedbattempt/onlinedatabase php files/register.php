<?php

$servername = "localhost";
$username = "id9361653_dldvirtuallab";
$server_password = "IndecipherablyObfuscatedTExT";
$dbName = "id9361653_dldvirtuallab";

$firstname = $_POST["firstname"];
$lastname = $_POST["lastname"];
$email = $_POST["email"];
$password= $_POST["password"];

$conn = new mysqli($servername, $username, $server_password, $dbName);

if(!$conn){
	die("Connection Failed" . mysqli_connect_error());
}

$emailcheckquery = "SELECT email FROM student_account WHERE email = '" . $email . "';";

$emailcheck = mysqli_query($conn, $emailcheckquery) or die("2: Email Check Query Failed");

$emailcheckqueryadmin = "SELECT email FROM admin_account WHERE email = '" . $email . "';";

$emailcheckadmin = mysqli_query($conn, $emailcheckqueryadmin) or die("2: Email Check Query Admin Failed");

if(mysqli_num_rows($emailcheck) > 0 || mysqli_num_rows($emailcheckadmin) > 0){
	echo "3: This email is already linked to an account";
	exit();
}

$salt = "\$5\$rounds-6000\$" . "BeanbeatsAndrew" . $firstname . "\$";

$hash = crypt($password, $salt);

$sql = "INSERT INTO student_account(firstname, lastname, email, salt, hash) VALUES('".$firstname."','".$lastname."', '".$email."','".$salt."','".$hash."')";

$result = mysqli_query($conn, $sql);

if(!$result){
	echo"There was an error";

}

else{
	echo"0";
}




?>