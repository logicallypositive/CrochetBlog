#!/bin/bash

# API Base URL
API_BASE="${API_BASE:-http://localhost:5000/api/posts}"

# Function for Creating a post
create() {
	curl -X POST "$API_BASE" \
		-H 'Content-Type: application/json' \
		-d '{
        "Rating": 3,
        "Title": "Coaster",
        "Description": "Super Cute",
        "Category": ["Home", "Useful"]
    }' | jq
}

# Function for Getting a Specific post
get() {
	local guid="$1"

	curl -X GET "$API_BASE/$guid" | jq
}

# Function for Getting All posts
getall() {
	curl -X GET "$API_BASE" | jq
}

# Function for Updating a post
update() {
	local guid="$1"

	curl -X PUT "$API_BASE/$guid" \
		-H 'Content-Type: application/json' \
		-d '{
        "Id": "'$guid'",
        "Rating": 1,
        "Title": "Updated post",
        "Description": "Updated Description",
        "Category": ["Christmas", "Art"]
    }' | jq
}

# Function for Deleting a post
delete() {
	local guid="$1"

	curl -X DELETE "$API_BASE/$guid" | jq
}

# Function for Validating a post request
validate() {
	# POST
	echo -e "\nTESTING POST REQUEST WITH WRONG RATING"
	curl -X POST "$API_BASE" \
		-H 'Content-Type: application/json' \
		-d '{
        "Rating": 7,
        "Title": "",
        "Description": "Super Cute",
        "Category": ["Home", "Useful"]
    }' | jq

	# GET
	echo -e "\nTESTING GET REQUEST WITH WRONG GUID"
	curl -X GET "$API_BASE/96bb9311-9757-4b4f-8f68-ca5983eec12c" | jq

}

# Main script execution
# Pass function name and arguments (if any) to run specific operations
"$@"
