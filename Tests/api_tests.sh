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
    }' | jq .
}

# Function for Getting a Specific post
get() {
    local guid="$1"
    [ -z "$guid" ] && { echo "GUID not provided"; return 1; }
    
    curl -X GET "$API_BASE/$guid" | jq .
}

# Function for Getting All posts
getall() {
    curl -X GET "$API_BASE" | jq .
}

# Function for Updating a post
update() {
    local guid="$1"
    [ -z "$guid" ] && { echo "GUID not provided"; return 1; }

    curl -X PUT "$API_BASE/$guid" \
    -H 'Content-Type: application/json' \
    -d '{
        "Id": "'$guid'",
        "Rating": 1,
        "Title": "Updated post",
        "Description": "Updated Description",
        "Category": ["Christmas", "Art"]
    }' | jq .
}

# Function for Deleting a post
delete() {
    local guid="$1"
    [ -z "$guid" ] && { echo "GUID not provided"; return 1; }

    curl -X DELETE "$API_BASE/$guid" | jq .
}

# Main script execution
# Pass function name and arguments (if any) to run specific operations
"$@"
