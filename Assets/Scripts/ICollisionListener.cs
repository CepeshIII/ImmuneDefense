public interface ICollisionListener
{
    public void CollisionHandler_OnCollisionEnter(object obj, CollisionHandlerArgs args);
    public void CollisionHandler_OnCollisionStay(object obj, CollisionHandlerArgs args);
    public void CollisionHandler_OnCollisionExit(object obj, CollisionHandlerArgs args);
}