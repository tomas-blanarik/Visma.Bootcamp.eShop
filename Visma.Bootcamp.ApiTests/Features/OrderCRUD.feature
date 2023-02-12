Feature: OrderCRUD

Scenario: GetAll - should succeed
	When I retrieve all orders
	Then I see successful response with status code-'200'

