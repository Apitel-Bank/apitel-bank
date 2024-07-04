package com.apitel_bank.main_banking_service.singletons;

import org.springframework.stereotype.Component;
import java.time.Instant;
import java.time.Duration;
import java.time.ZoneId;
import java.time.ZonedDateTime;
import java.util.concurrent.atomic.AtomicBoolean;

@Component
public class GameState {
    private Instant startTime;
    private Instant currentTime;
    private final AtomicBoolean running = new AtomicBoolean(false);
    private static final long REAL_WORLD_MINUTES_PER_GAME_DAY = 2;
    private static final long MILLIS_PER_MINUTE = 60000;

    public void startGame(Instant start) {
        this.startTime = start;
        this.currentTime = start;
        running.set(true);
    }

    public void resetGame(Instant start) {
        this.startTime = start;
        this.currentTime = start;
    }

    public ZonedDateTime getCurrentGameTime() {
        if (running.get()) {
            long elapsedMillis = Duration.between(startTime, Instant.now()).toMillis();
            long gameDaysElapsed = elapsedMillis / (REAL_WORLD_MINUTES_PER_GAME_DAY * MILLIS_PER_MINUTE);
            long gameMonthsElapsed = gameDaysElapsed / 30;
            return ZonedDateTime.ofInstant(startTime, ZoneId.systemDefault()).plusDays(gameMonthsElapsed * 30);
        } else {
            return ZonedDateTime.ofInstant(currentTime, ZoneId.systemDefault());
        }
    }
}
