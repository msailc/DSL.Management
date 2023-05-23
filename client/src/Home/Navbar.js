import React from 'react';
import { useEffect } from 'react';



function Navbar(props) {

  return (
    <nav>
      <div className="navbar-left">
        <p>DSL Management</p>
      </div>
      <div className="navbar-right">
        <p>Logged in as {props.username}</p>
      </div>
    </nav>
  );
}

export default Navbar;

