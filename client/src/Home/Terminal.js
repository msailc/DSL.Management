import "./Terminal.css";

export default function Terminal(){
    return(
    <div className="terminal">
      <div className="terminal__line">Write your commands below</div>
      <div className="terminal__prompt">
        <div className="terminal__prompt__label">HardCodeIme:</div>
        <div className="terminal__prompt__input">
          <input type="text" />
        </div>
      </div>
    </div>
    )
}