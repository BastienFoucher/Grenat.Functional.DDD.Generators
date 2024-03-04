//HintName: BuilderGeneratorTests.Cart.g.cs
//generation count: 1
namespace BuilderGeneratorTests;
public static class CartSetters
{
    public static Entity<Cart> SetCouponId(this Cart cart, Option<Int32> couponId)
    {
        return cart.SetOption(CouponId, static (cart, couponId) => cart with { CouponId = couponId });
    }

    public static Entity<Cart> SetCouponId(this Entity<Cart> cart, Option<Int32> couponId)
    {
        return cart.SetOption(CouponId, static (cart, couponId) => cart with { CouponId = couponId });
    }

    public static Entity<Cart> SetCoupon(this Cart cart, Option<Entity<Coupon>> coupon)
    {
        return cart.SetOption(Coupon, static (cart, coupon) => cart with { Coupon = coupon });
    }

    public static Entity<Cart> SetCoupon(this Entity<Cart> cart, Option<Entity<Coupon>> coupon)
    {
        return cart.SetOption(Coupon, static (cart, coupon) => cart with { Coupon = coupon });
    }

}