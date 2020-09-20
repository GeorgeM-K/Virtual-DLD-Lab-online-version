<?php

$servername = "localhost";
$username = "id9361653_dldvirtuallab";
$server_password = "IndecipherablyObfuscatedTExT";
$dbName = "id9361653_dldvirtuallab";

$sectionName = $_POST["sectionName"];


$conn = new mysqli($servername, $username, $server_password, $dbName);

if(!$conn){
	die("Connection Failed" . mysqli_connect_error());
}

$namecheckquery = "SELECT sectionName FROM sections WHERE sectionName = '" . $sectionName . "';";

$namecheck = mysqli_query($conn, $namecheckquery) or die("2: Name Check Query Failed");


if(mysqli_num_rows($namecheck) == 0){
	echo "3: This section does not exist";
	exit();
}

$sql = "DELETE FROM sections WHERE sectionName = '" . $sectionName . "';";

$result = mysqli_query($conn, $sql);

if(!$result){
	echo"There was an error";

}

else{
	echo"0";
}




?>