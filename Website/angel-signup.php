<!doctype html>
<html lang="en">
  <head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous">

    <title>Angel Application</title>
  </head>
  <body>
    <?php
      include "db.php";
    ?>
    <?php include "nav.php"?>
    <h1>Application</h1>

    <form method="post" action="thank-you.php" style="margin-left:25px;">
      <input type="text" name="usrID" />
        <div class="form-group">
            <label for="firstName">First Name</label>
            <input type="text" class="form-control" id="firstName" name = "usrFName" aria-describedby="firstName" placeholder="First Name">
        </div>
        <div class="form-group">
            <label for="lastName">Last Name</label>
            <input type="text" class="form-control" id="lastName" name = "usrLName" aria-describedby="lastName" placeholder="Last Name">
        </div>
        <div class="form-group">
            <label for="maidenName">Maiden Name</label>
            <input type="text" class="form-control" id="maidenName" name = "angelMaidenName" aria-describedby="maidenName" placeholder="Maiden Name">
        </div>
        <div class="form-group">
            <label for="address">Address</label>
            <input type="text" class="form-control" name="usrAddress" id="adress" aria-describedby="address" placeholder="Address">
        </div>
        <div class="form-group">
            <label for="city">City</label>
            <input type="text" class="form-control" id="city" name="usrCity" aria-describedby="city" placeholder="City">
        </div>
        <div class="form-group">
            <label for="state">State</label>
            <select class="form-control" id="state" name="usrState" >
                <option value="AL">Alabama</option>
                <option value="AK">Alaska</option>
                <option value="AZ">Arizona</option>
                <option value="AR">Arkansas</option>
                <option value="CA">California</option>
                <option value="CO">Colorado</option>
                <option value="CT">Connecticut</option>
                <option value="DE">Delaware</option>
                <option value="DC">District Of Columbia</option>
                <option value="FL">Florida</option>
                <option value="GA">Georgia</option>
                <option value="HI">Hawaii</option>
                <option value="ID">Idaho</option>
                <option value="IL">Illinois</option>
                <option value="IN">Indiana</option>
                <option value="IA">Iowa</option>
                <option value="KS">Kansas</option>
                <option value="KY">Kentucky</option>
                <option value="LA">Louisiana</option>
                <option value="ME">Maine</option>
                <option value="MD">Maryland</option>
                <option value="MA">Massachusetts</option>
                <option value="MI">Michigan</option>
                <option value="MN">Minnesota</option>
                <option value="MS">Mississippi</option>
                <option value="MO">Missouri</option>
                <option value="MT">Montana</option>
                <option value="NE">Nebraska</option>
                <option value="NV">Nevada</option>
                <option value="NH">New Hampshire</option>
                <option value="NJ">New Jersey</option>
                <option value="NM">New Mexico</option>
                <option value="NY">New York</option>
                <option value="NC">North Carolina</option>
                <option value="ND">North Dakota</option>
                <option value="OH">Ohio</option>
                <option value="OK">Oklahoma</option>
                <option value="OR">Oregon</option>
                <option value="PA">Pennsylvania</option>
                <option value="RI">Rhode Island</option>
                <option value="SC">South Carolina</option>
                <option value="SD">South Dakota</option>
                <option value="TN">Tennessee</option>
                <option value="TX">Texas</option>
                <option value="UT">Utah</option>
                <option value="VT">Vermont</option>
                <option value="VA">Virginia</option>
                <option value="WA">Washington</option>
                <option value="WV">West Virginia</option>
                <option value="WI">Wisconsin</option>
                <option value="WY">Wyoming</option>
            </select>
        </div>
        <div class="form-group">
            <label for="zip"></label>
            <input type="text" class="form-control" name="usrZip" id="zip" aria-describedby="zip" placeholder="Zip">
        </div>
        <div class="form-group">
            <label for="phone"></label>
            <input type="text" class="form-control" name="usrPhone" id="phone" aria-describedby="phone" placeholder="Phone">
        </div>
        <div class="form-group">
            <label for="email"></label>
            <input type="email" class="form-control" id="email" name="usrEmail" aria-describedby="email" placeholder="Email">
        </div>
        <div class="form-group">
          <label for="reasons">Reasons for Wanting to Be An Angel</label>
          <textarea class="form-control" id="reasons" name="angelReasons" rows="3"></textarea>
        </div>
        <div class="form-group">
          <label for="experience">Previous Experience in Helping Others in Crisis</label>
          <textarea class="form-control" id="experience" name="angelPrevExp" rows="3"></textarea>
        </div>
        <div class="form-group">
          <label for="volunteerExp"></label>Volunteer History / Experience</label>
          <textarea class="form-control" id="volunteerExp" name="angelHistExp" rows="3"></textarea>
        </div>

        <div class="form-group">
          <label for="criminal"></label>History of criminal /legal convictions</label>
          <textarea class="form-control" name="angelCriminalHist" id="criminal" rows="3"></textarea>
        </div>
        <div class="form-check">
          <input class="form-check-input" type="checkbox" value="backGroundCheck" name="angelBackCheck" id="checkBackground">
          <label class="form-check-label" for="checkBackground">
            Permission to do a background check
          </label>
        </div>







        <button type="submit" class="btn btn-primary">Submit</button>
    </form>

    <!-- Optional JavaScript -->
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script>
  </body>
</html>
