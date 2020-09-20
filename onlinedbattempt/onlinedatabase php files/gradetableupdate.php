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

$classcheckquery = "SELECT email, preLabOne, preLabTwo, preLabThree, preLabFour, preLabFive, labOne, labTwo , labThree, labFour, labFive, postLabOne, postLabTwo, postLabThree, postLabFour, postLabFive FROM student_account WHERE class = '" . $class . "';";

$classcheck = mysqli_query($conn, $classcheckquery) or die("2: Class Check Query Failed");

if(!$classcheck){
	echo"10";

}

if(mysqli_num_rows($classcheck) > 0){
	while($row = mysqli_fetch_assoc($classcheck)){
		
		echo"$row[email]" . "|" . "$row[preLabOne]" . "|" . "$row[preLabTwo]" . "|" . "$row[preLabThree]" . "|" . "$row[preLabFour]" . "|" . "$row[preLabFive]" . "|" . "$row[labOne]" . "|" . "$row[labTwo]" . "|" . "$row[labThree]" . "|" . "$row[labFour]" . "|" . "$row[labFive]" . "|" . "$row[postLabOne]" . "|" . "$row[postLabTwo]" . "|" . "$row[postLabThree]" . "|" . "$row[postLabFour]" . "|" . "$row[postLabFive]" . "|";
	}
}


//'" . $sectionName . "'

//".$row['sectionName']"

?>