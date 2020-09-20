<?php

$servername = "localhost";
$username = "id9361653_dldvirtuallab";
$server_password = "IndecipherablyObfuscatedTExT";
$dbName = "id9361653_dldvirtuallab";

$conn = new mysqli($servername, $username, $server_password, $dbName);

if(!$conn){
	die("Connection Failed" . mysqli_connect_error());
}

$namecheckquery = "SELECT sectionName FROM sections";

$namecheck = mysqli_query($conn, $namecheckquery) or die("2: Name Check Query Failed");

if(!$namecheck){
	echo"10";

}

if(mysqli_num_rows($namecheck) > 0){
	while($row = mysqli_fetch_assoc($namecheck)){
		
		echo"$row[sectionName]" . "|";
	}
}


//'" . $sectionName . "'

//".$row['sectionName']"

?>