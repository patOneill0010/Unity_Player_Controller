public class Proxy_Sprint : IDynamicProxies, IInputProxies, IProxyDependent {
    private PlayerStateController _StateController;
    private Stat_Sprint _SprintStat;
    private bool _InputActive;
    private bool _IsToggled;
    private bool _ToggleProxyActive;

    public Proxy_Sprint(PlayerStateController stateController, Stat_Sprint sprintStat) {
        _StateController = stateController;
        _InputActive = false;
        _IsToggled = true;
        _ToggleProxyActive = false;
        _SprintStat = sprintStat;
    }

    public void InputActive() {
        /**
         * @desc The input for this proxy is active 
        */
        _InputActive = true;
        if (_IsToggled) { //Check to see if the input is set to toggoled
            _ToggleProxyActive = !_ToggleProxyActive; //Monitor whether the proxy's state is active or inactive
        }
        CheckActivation();
    }

    public void InputInactive() {
        /**
         * @desc The input for this proxy is no longer active
        */
        _InputActive = false;
        CheckActivation();
    }

    public void CheckActivation() {
        /**
          * @desc Ensure this proxy is active/inactive as its internal parms changes and external parameters change as well
        */
        if (_IsToggled) { //If player has set sprint to toggle, handle toggle sprint event
            if (_ToggleProxyActive && _SprintStat.CheckStaminaCostSprint()) {
                SendRequest();
            } else {
                RetractRequest();
            }
        } else { //If player has set spint to press and hold, handle press and hold event
            if (_InputActive && _SprintStat.CheckStaminaCostSprint()) {
                SendRequest();
            } else {
                RetractRequest();
            }
        }
    }

    public void SendRequest() {
        /**
          * @desc Tell the current state that it should transition to a sprinting state if such a path existed in the state machine
        */
        _StateController.GetActiveState().Sprint();
    }

    public void RetractRequest() {
        /**
          * @desc Tell the current state that it should transition out of a sprinting state if such a path existed in the state machine 
        */
        _StateController.GetActiveState().CancelSprint();
    }

    public void EnableToggleToSprint() {
        /**
          * @desc Enable toggle to sprint
        */
        _IsToggled = true;
    }

    public void DisableToggleToSprint() {
        /**
          * @desc Disable toggle to sprint and set _ToggleProxyActive to false
        */
        _IsToggled = false;
        _ToggleProxyActive = false; //If the player disables toggle to sprint while actively sprinting, set _ToggleProxyActive to false to ensure first sprint active always causes the player to sprint if toggle to sprint is re-enabled
    }

    public void ExternalProxyKill() {
        /**
          * @desc The sprint proxy is a proxy that depends on the continuous activation of other proxies, if said proxies are turned inactive, sprint proxy most become inactive as well
        */
        if (_ToggleProxyActive) { //Check to see if the user is toggle sprinting before setting the proxy to be inactive
            _ToggleProxyActive = false;
        }
    }
}