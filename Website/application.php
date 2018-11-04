<!doctype html>
<html lang="en">
  <head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous">
    <?php include "db.php"?>
    <title>Applicant</title>
  </head>
  <body>
    <?php include "nav.php"?>
    <h1>Applicant</h1>
    <div style="margin-left: 25px">


    <?php

        $theId = $_GET['id'];
        $qry = "select * from angels, accountuser where angels.usrID = accountuser.usrID and accountuser.usrID = '{$theId}'";

                $statement = $db->prepare($qry);
                $statement = $db->prepare($qry);
                $statement->execute();
                $angel = $statement->fetch();

                echo "<p>". $angel['usrFName'] . " " . $angel['usrLName'] . "</p>";
                echo "<p>". $angel['angelMaidenName'] . "</p>";
                echo "<p>". $angel['usrAddress'] . "</p>";
                echo "<p>". $angel['usrCity'] . ",". $angel['usrCity']. " " . $angel['usrCity'] . " "  . $angel['usrZip']." </p>";
                echo "<p>". $angel['angelReasons'] . "</p>";
                echo "<p>". $angel['angelPrevExp'] . "</p>";
                echo "<p>". $angel['angelHistExp'] . "</p>";
                echo "<p>". $angel['angelCriminalHist'] . "</p>";
                if ($angel['angelBackCheck'] == 1){
                  echo "Ok to do background check";
                } else {
                  echo "Applicant denied background check";
                }



    ?>


      <form action="angels.php" method="post">
        <div class="form-check">
            <input class="form-check-input" type="radio" name="selected" id="pending" value="pending"
            <?php
              if ($angel['angelApproved'] == 2){
                echo "checked";
              }
            ?>
            >
            <label class="form-check-label" for="pending">
              Pending
            </label>
        </div>
        <div class="form-check">
            <input class="form-check-input" type="radio" name="selected" id="approved" value="approved"
            <?php
              if ($angel['angelApproved'] == 1){
                echo "checked";
              }
            ?>

            >
            <label class="form-check-label" for="approved">
              Approved
            </label>
        </div>
        <div class="form-check">
          <input class="form-check-input" type="radio" name="selected" id="rejected" value="rejected"
          <?php
            if ($angel['angelApproved'] == 0){
              echo "checked";
            }
          ?>

          >
          <label class="form-check-label" for="rejected">
            Not Approved
          </label>
        </div>
        <div class="form-group">
            <label for="comments">Reviewer Comments</label>
            <textarea class="form-control" id="comments" name="comments" rows="3"><?php echo $angel['reviewerComments']?></textarea>
        </div>
          <input type="hidden" name="id" value= '<?php echo $_GET['id'] ?>'>
          <input type ="submit" value="Submit" />
      </form>
    </div>
    <!-- Optional JavaScript -->
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script>
  </body>
</html>
