<?php
  include "db.php";
  if ($_POST){
    $approved = 0;
    if ($_POST['selected'] == "approved"){
      $approved = 1;
    } else if ($_POST['selected'] == "pending"){
      $approved = 2;
    }
    $qry = "update angels  set angelApproved = {$approved}, reviewerComments = '{$_POST['comments']}' where usrID = '{$_POST['id']}'" ;
    
    $statement = $db->prepare($qry);
    $statement->execute();

  }
 ?>
<!doctype html>
<html lang="en">
  <head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous">

    <title>Approval</title>
    <?php
      include "db.php";
    ?>
  </head>
  <body>
    <?php include "nav.php"?>
    <h1>Angel Approvals</h1>
    <h2>Pending Angels</h2>
    <div style="maring-left:25px">


    <?php
      $qry = "select * from accountuser, angels where accountuser.usrID = angels.usrID and angelApproved = 2; ";
      $statement = $db->prepare($qry);
      $statement->execute();
      $angels = $statement->fetchAll();
      $statement->closeCursor();

     foreach ($angels as $angel){
        echo ("<a href='application.php?id={$angel['usrID']}'> {$angel['usrFName']} {$angel['usrLName']} </a><br>");
     }
     ?>

     <h2>Approved Angels</h2>
     <?php
       $qry = "select * from accountuser, angels where accountuser.usrID = angels.usrID and angelApproved = 1; ";
       $statement = $db->prepare($qry);
       $statement->execute();
       $angels = $statement->fetchAll();
       $statement->closeCursor();

      foreach ($angels as $angel){
         echo ("<a href='application.php?id={$angel['usrID']}'> {$angel['usrFName']} {$angel['usrLName']} </a><br>");
      }
      ?>

      <h2>Declined Angels</h2>
      <?php
        $qry = "select * from accountuser, angels where accountuser.usrID = angels.usrID and angelApproved = 0; ";
        $statement = $db->prepare($qry);
        $statement->execute();
        $angels = $statement->fetchAll();
        $statement->closeCursor();

       foreach ($angels as $angel){
          echo ("<a href='application.php?id={$angel['usrID']}'> {$angel['usrFName']} {$angel['usrLName']} </a><br>");
       }
       ?>
       </div>
    <!-- Optional JavaScript -->
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script>
  </body>
</html>
