import React from 'react';
import { Link } from 'react-router-dom';
import { useEffect } from 'react';
import { FaUser } from 'react-icons/fa'; 
function Navbar(props) {
  return (
    <nav>
      <div className="navbar-left">
        <p>DSL Management</p>
      </div>
      <div className="navbar-right">
        <FaUser className="user-icon" /> {}
        <p>Logged in as {props.username}</p>
      </div>
    </nav>
  );
}

export default Navbar;
