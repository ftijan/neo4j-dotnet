# neo4j .Net App Example
A quick sample app showing how to:
- Run a local neo4j instance from a Docker compose file
- Connect to the local neo4j instance 
- Create a session
- Run a simple query
- Map the data

Based on a longer neo4j .Net sample app found [here](https://github.com/neo4j-examples/movies-dotnetcore-bolt/)

To recreate the sample `Movies` dataset:
- Make sure the docker image instance is running
- Navigate to the local neo4j DB browser app at [http://localhost:7474](http://localhost:7474)
- From the left-hand menu, navigate to the `Neo4j Browser Guides` page
- Select the `:guide movie-graph` section link and follow the instructions