<?php

$servername = "localhost";
$username = "id9361653_dldvirtuallab";
$server_password = "IndecipherablyObfuscatedTExT";
$dbName = "id9361653_dldvirtuallab";

$name = $_POST["sectionName"];


$conn = new mysqli($servername, $username, $server_password, $dbName);

if(!$conn){
	die("Connection Failed" . mysqli_connect_error());
}

$namecheckquery = "SELECT Name FROM sections WHERE sectionName = '" . $name . "';";

$namecheck = mysqli_query($conn, $namecheckquery) or die("2: Email Check Query Failed");


if(mysqli_num_rows($namecheck) > 0){
	echo "3: This section already exists";
	exit();
}

$sql = "INSERT INTO sections(Name) VALUES('".$name."')";

$result = mysqli_query($conn, $sql);

if(!$result){
	echo"There was an error";

}

else{
	echo"0";
}




?>