Feature: Test scenarios of API https://jsonplaceholder.typicode.com/posts

  @get_by_id
  Scenario: View user by ID
    Given I send a GET request by ID
    Then the response status code should be "200"
    And the response body should contain existing user data

  @post
  Scenario: Create a new user
    Given I send a POST request
    Then the response status code should be "201"
    And the response body should contain new user data