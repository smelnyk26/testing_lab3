Feature: Booking API

  Scenario: Create a new booking
    Given I am authenticated
    When I create a new booking with the following details
      | firstname | lastname | totalprice | depositpaid | checkin     | checkout    | additionalneeds |
      | John      | Doe      | 150        | true        | 2023-01-01  | 2023-01-10  | Breakfast       |
    Then the booking should be created successfully
