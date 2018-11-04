<?php
// **** PDO Syntax


//**************************

// $dsn = 'mysql:host=localhost;dbname=borrowmyangel';
//
// $username = "root";
// $password = "kailua";

//
$dbhost = 'borrowmyangel.ckwia8qkgyyj.us-east-1.rds.amazonaws.com';
$dbport = '3306';
$dbname = 'borrowMyAngel';


$dsn = "mysql:host={$dbhost};port={$dbport};dbname={$dbname};";
$username = 'b0rrowMyAng3l';
$password ='U8QZW9jU0be1';

try{
   $db = new PDO($dsn, $username, $password);

  // echo "connected";
} catch (PDOException $e){
   echo "Didn't connect";
   echo $e->getMessage();
}

            // $username = 'root';
            // $password = 'kailua';
            // $host = 'localhost';
          //   $username = 'b0rrowMyAng3l';
          //   $password = 'U8QZW9jU0be1';
          //   $host = 'borrowmyangel.cyzow4dnhc8w.us-east-2.rds.amazonaws.com';
          //   $db_name = 'borrowmyangel';
          //   $port_num = 3306;
          //   // $link = mysqli_connect('mydbinstance.abcdefghijkl.us-east-1.rds.amazonaws.com', 'sa', 'mypassword', 'mydb', 3306);
          //   $db = mysqli_connect($host, $username,$password, $db_name, $port_num);
          //
          // $connection_error = mysqli_connect_error();
          //  if ($connection_error != null){
          //      echo "<p> error connection: $connection_error</p>";
          //
          //  } else {
          //       echo "good to go   ";
          //   }
?>
