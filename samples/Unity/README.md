# Pluralinput.Sdk Unity sample

Initializing Pluralinput within the Unity Editor window might cause it to crash.
Until this issue is fixed, please guard the InputManager initialization using the `UNITY_EDITOR` directive (see Startup.cs for an example).