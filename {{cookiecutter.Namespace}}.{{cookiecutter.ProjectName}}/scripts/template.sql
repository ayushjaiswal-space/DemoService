do $$
declare
    migrationId text := null; -- PUT YOUR UNIQUE MIGRATION ID HERE
begin -- Begin transaction. No need to explicitly commit or rollback
    -- Create migration history table
    create table if not exists "__MigrationsHistory" (
        "MigrationId" text not null constraint "PK___MigrationsHistory" primary key,
        "Timestamp" timestamp with time zone not null
    );

    -- Insert current migration id into migration history table
    insert into "__MigrationsHistory" values (migrationId, now());

    -- PUT YOUR SCRIPT HERE
	
    --
end $$;