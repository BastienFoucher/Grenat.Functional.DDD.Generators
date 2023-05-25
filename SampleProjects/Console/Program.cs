var validCart = new CartBuilder()
                .WithId("1ds3d!bM")
                .WithItems(ImmutableList<Entity<CartItem>>.Empty
                    .Add(
                        new CartItemBuilder()
                        .WithId("45xxsDg1=")
                        .WithProductId("ne252TJqAWk3")
                        .WithAmount(25, "EUR")
                        .Build())
                    .Add(
                        new CartItemBuilder()
                        .WithId("784dfg1=")
                        .WithProductId("s4ysc9DneP8")
                        .WithAmount(50, "EUR")
                        .Build()))
                .WithTotalAmount(75, "EUR")
                .Build()
                .Match(
                    Valid: c =>
                    {
                        Console.WriteLine(c);
                        Console.WriteLine(c.Items);
                        return Entity<Cart>.Valid(c);
                    },
                    Invalid: e =>
                    {
                        return Entity<Cart>.Invalid(e);
                    });

var cartInErrorWithHarvestedErrors = new CartBuilder()
                .WithId("1ds3d!bM")
                .WithItems(ImmutableList<Entity<CartItem>>.Empty
                    .Add(
                        new CartItemBuilder()
                        .WithId("45xxsDg1=")
                        .WithProductId("")
                        .WithAmount(25, "EUR")
                        .Build())
                    .Add(
                        new CartItemBuilder()
                        .WithId("s4ysc9DneP8s4ysc9DneP8")
                        .WithProductId("")
                        .WithAmount(2600, "EUR")
                        .Build()))
                .WithTotalAmount(2625, "EUR")
                .Build()
                .Match(
                    Valid: c =>
                    {
                        return Entity<Cart>.Valid(c);
                    },
                    Invalid: errors =>
                    {
                        foreach (var e in errors) Console.WriteLine(e.Message);
                        return Entity<Cart>.Invalid(errors);
                    });


Console.ReadLine();
