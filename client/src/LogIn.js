import React from 'react';
import { Link } from 'react-router-dom';

function LogIn() {
  return (
    <div>
      <h1>Login Page</h1>
      <button>
        <Link to="/home">Log In With GitHub</Link>
      </button>
    </div>
  );
}

export default LogIn;