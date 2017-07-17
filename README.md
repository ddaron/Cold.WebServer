# FrostigBytes C# WebServer
[![Build Status](https://travis-ci.org/ddaron/Cold.WebServer.svg?branch=master)](https://travis-ci.org/ddaron/Cold.WebServer)

Basic idea for the server is [from this dailyprogrammer post](https://www.reddit.com/r/dailyprogrammer/comments/6lti17/20170707_challenge_322_hard_static_http_server/). The project is in education form only, most likely project will not ever see release. Educational value is mostly for design patterns and knowing how the internals of a webserver work (custom implementation - no looking at the sources).


### Todo list

* [ ] Load files from disk.
* [ ] ANY exception handling.
* [ ] HTTP logger
* [ ] Template engine
* [ ] Configuration: List directory
* [ ] Listing directories
* [ ] Error pages
* [ ] Server header
* [ ] Support for content-length
* [ ] GET should load static files 
* [ ] Different mime types
* [ ] Add support for other HTTP verbs
* [ ] Default error pages
    * [ ] 404
    * [ ] 403
    * [ ] 500
    * [ ] Other
* [ ] Configuration: Customizable error pages
* [ ] Tests 

### Future:

* [ ] Proxy mod
* [ ] Reverse proxy
* [ ] Authentication (Access Management System FSAM - Frostig Security Access Manager)
   * [ ] User DB
   * [ ] Auth page
   * [ ] Endpoint for application
   * [ ] User management page
