import React from 'react';
import { Link } from 'react-router-dom';

function Navbar() {
  return (
    <nav>
      <div className="navbar-left">
        <p>DSL Management</p>
      </div>
      <div className="navbar-right">
        <p>Logged in as User</p>
      </div>
    </nav>
  );
}

export default Navbar;

