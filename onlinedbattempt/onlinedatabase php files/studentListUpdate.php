<?php

$servername = "localhost";
$username = "id9361653_dldvirtuallab";
$server_password = "IndecipherablyObfuscatedTExT";
$dbName = "id9361653_dldvirtuallab";

$class = $_POST["class"];

$conn = new mysqli($servername, $username, $server_password, $dbName);

if(!$conn){
	die("Connection Failed" . mysqli_connect_error());
}

$classcheckquery = "SELECT email FROM student_account WHERE class = '" . $class . "';";

$classcheck = mysqli_query($conn, $classcheckquery) or die("2: Class Check Query Failed");

if(!$classcheck){
	echo"10";

}

if(mysqli_num_rows($classcheck) > 0){
	while($row = mysqli_fetch_assoc($classcheck)){
		
		echo"$row[email]" . "|";
	}
}


//'" . $sectionName . "'

//".$row['sectionName']"

?>