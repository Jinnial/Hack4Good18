<?php

function sendAngelData(){
  include "db.php";



  $qry = "update accountuser set usrFName = '{$_POST['usrFName']}', usrLName='{$_POST['usrLName']}', usrEmail = '{$_POST['usrEmail']}', usrAddress='{$_POST['usrAddress']}', usrCity='{$_POST['usrCity']}',  usrState='{$_POST['usrState']}', usrZip='{$_POST['usrZip']}', usrPhone='{$_POST['usrPhone']}' where usrID = '{$_POST['usrID']}'";
  $statement = $db->prepare($qry);
  $statement->execute();


  $qry = "insert into angels (usrID, angelReasons, angelPrevExp, angelHistExp, angelCriminalHist, angelMaidenName) values ('{$_POST['usrID']}', '{$_POST['angelReasons']}', '{$_POST['angelPrevExp']}','{$_POST['angelHistExp']}', '{$_POST['angelCriminalHist']}', '{$_POST['angelMaidenName']}' )";
  $statement = $db->prepare($qry);
  $statement->execute();

  if ($_POST['angelBackCheck']){
    $qry = "update angels set angelBackCheck = 1 where usrID = '{$_POST['usrID']}'";
    $statement = $db->prepare($qry);
    $statement->execute();
  }


}
 ?>
