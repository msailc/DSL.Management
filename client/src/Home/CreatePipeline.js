import React, { useState } from 'react';
import axios from 'axios';

const PipelineModal = () => {
  const [showModal, setShowModal] = useState(false);
  const [name, setName] = useState('');
  const [pipelineCommands, setPipelineCommands] = useState('');
  const [existingCommands, setExistingCommands] = useState([]);
  const [showCommands, setShowCommands] = useState(false);

  const handleNewPipelineClick = () => {
    setShowModal(true);
  };

  const handleNameChange = (e) => {
    setName(e.target.value);
  };
  const showToast = (message) => {
    alert(message);
  };

  const handlePipelineCommandsChange = (e) => {
    setPipelineCommands(e.target.value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (name.trim() === '') {
      showToast('Name field is empty');
      return;
    }

    if (existingCommands.length === 0) {
      showToast('Add at least one command');
      return;
    }

    const apiUrl = 'http://localhost:5017/pipeline';

    try {
      const payload = {
        name: name,
        steps: existingCommands.map((command) => ({
          command: command,
          parameters: [],
        })),
      };

      const response = await axios.post(apiUrl, payload);
      console.log('API Response:', response.data);

      // Reset the form
      setName('');
      setPipelineCommands('');

      // Close the modal
      setShowModal(false);
      setExistingCommands([]);
    } catch (error) {
      console.error('API Error:', error);
      showToast('Failed to create the pipeline');
    }
  };

  const closeModal = () => {
    setName('');
    setPipelineCommands('');
    setShowModal(false);
    setExistingCommands([]);
  };

  const addCommandHandler = () => {
    if (pipelineCommands.trim() === '') {
      return;
    }
    setExistingCommands([...existingCommands, pipelineCommands]);
    setShowCommands(true);
    setPipelineCommands('');
    console.log(existingCommands);
  };

  const deleteLastCommandHandler = () => {
    if (existingCommands.length < 2) {
      setExistingCommands(existingCommands.slice(0, -1));
      setShowCommands(false);
    } else {
      setExistingCommands(existingCommands.slice(0, -1));
    }
  };

  return (
    <div>
      <button onClick={handleNewPipelineClick}>New Pipeline</button>
      {showModal && (
        <div className="modal">
          <div className="modal-content">
            <h2>Create New Pipeline</h2>
            <form onSubmit={handleSubmit}>
              <div>
                <label htmlFor="name">Name:</label>
                <input
                  type="text"
                  id="name"
                  value={name}
                  onChange={handleNameChange}
                />
              </div>
              <div>
                <label htmlFor="pipelineCommands">Pipeline Commands:</label>
                {showCommands && (
                  <div>
                    <ul>
                      {existingCommands.map((command, index) => (
                        <li key={index}>{command}</li>
                      ))}
                    </ul>
                    <button type="button" onClick={deleteLastCommandHandler}>
                      -
                    </button>
                  </div>
                )}
                <input
                  type="text"
                  id="pipelineCommands"
                  value={pipelineCommands}
                  onChange={handlePipelineCommandsChange}
                />
                <button type="button" onClick={addCommandHandler}>
                  +
                </button>
              </div>
              <div>
                <button type="submit">Create Pipeline</button>
                <button onClick={closeModal}>Close</button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
};

export default PipelineModal;
