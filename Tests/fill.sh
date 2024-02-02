#!/bin/bash

# Assuming the JSON is stored in a file named 'postsData.json'
json_file="postsData.json"
API_BASE="${API_BASE:-http://localhost:5000/api/posts}"

# Read each object from the JSON array
jq -c '.[]' "$json_file" | while read -r obj; do
  # Extract the required properties
  title=$(echo "$obj" | jq -r '.Title')
  description=$(echo "$obj" | jq -r '.Description')
  rating=$(echo "$obj" | jq -r '.Rating')
  category=$(echo "$obj" | jq -c '.Category')  #| join(", ") # Joining categories with comma
  imageurl=$(echo "$obj" | jq -r '.ImageUrl')

  # Construct and execute the HTTP request
  curl -X POST "$API_BASE" \
       -H "Content-Type: application/json" \
       -d "{
         \"Title\": \"$title\", 
         \"Description\": \"$description\", 
         \"Rating\": $rating, 
         \"Category\": $category, 
         \"ImageUrl\": \"$imageurl\"
       }" | jq
done

