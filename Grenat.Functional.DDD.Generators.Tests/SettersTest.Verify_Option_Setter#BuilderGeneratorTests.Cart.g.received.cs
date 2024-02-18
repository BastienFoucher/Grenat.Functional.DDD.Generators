//HintName: BuilderGeneratorTests.Cart.g.cs
//generation count: 1
namespace BuilderGeneratorTests;
public static partial record CartSetters
{
    public static Cart SetCouponId(this Cart cart, Option<Int32> couponId)
    {
        return couponId with { CouponId = couponId };
    }

    public static Entity<Cart> SetCouponId(this Cart cart, Option<Int32> couponId)
    {
        return couponId.SetOption(CouponId, static (cart, couponId) => cart with { CouponId = couponId });
    }

    public static Entity<Cart> SetCouponId(this Entity<Cart> cart, Option<Int32> couponId)
    {
        return couponId.SetOption(CouponId, static (cart, couponId) => cart with { CouponId = couponId });
    }

    public static Cart SetCoupon(this Cart cart, Option<Coupon> coupon)
    {
        return coupon with { Coupon = coupon };
    }

    public static Entity<Cart> SetCoupon(this Cart cart, Option<Entity<Coupon>> coupon)
    {
        return coupon.SetOption(Coupon, static (cart, coupon) => cart with { Coupon = coupon });
    }

    public static Entity<Cart> SetCoupon(this Entity<Cart> cart, Option<Entity<Coupon>> coupon)
    {
        return coupon.SetOption(Coupon, static (cart, coupon) => cart with { Coupon = coupon });
    }

}