<?php

$servername = "localhost";
$username = "id9361653_dldvirtuallab";
$server_password = "IndecipherablyObfuscatedTExT";
$dbName = "id9361653_dldvirtuallab";

$conn = new mysqli($servername, $username, $server_password, $dbName);

if(!$conn){
	die("Connection Failed" . mysqli_connect_error());
}

$Answercheckquery = "SELECT Answer FROM FAQ";

$Answercheck = mysqli_query($conn, $Answercheckquery) or die("2: Name Check Query Failed");

if(!$Answercheck){
	echo"11";

}

if(mysqli_num_rows($Answercheck) > 0){
	while($row = mysqli_fetch_assoc($Answercheck)){
		
		echo"$row[Answer]" . "|";
	}
}


//'" . $sectionName . "'

//".$row['sectionName']"

?>